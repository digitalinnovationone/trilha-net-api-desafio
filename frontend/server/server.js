const express = require("express");
const mongoose = require("mongoose");
const cors = require("cors");
const bcrypt = require("bcryptjs");
const jwt = require("jsonwebtoken");
const User = require("./models/User");
const Product = require("./models/Product");

const app = express();
app.use(cors());
app.use(express.json());

const ADMIN_EMAIL = "admin@exemplo.com";

mongoose.connect("mongodb://localhost:27017/pelebrasileira");

/////////////////////
// Middleware JWT
/////////////////////

const verifyToken = (req, res, next) => {
    const authHeader = req.headers.authorization;
    if (!authHeader) return res.status(401).json({ message: "Token não fornecido" });

    const token = authHeader.split(" ")[1];
    if (!token) return res.status(401).json({ message: "Token inválido" });

    try {
        const decoded = jwt.verify(token, "secret");
        req.user = decoded; // terá { id, email }
        next();
    } catch (err) {
        return res.status(403).json({ message: "Token inválido ou expirado" });
    }
};

/////////////////////
// Registro e Login
/////////////////////

app.post("/api/register", async (req, res) => {
    const { email, password } = req.body;
    try {
        const hashedPassword = await bcrypt.hash(password, 10);
        const user = await User.create({ email, password: hashedPassword });
        res.status(201).json({ message: "Usuário criado", user });
    } catch (error) {
        res.status(400).json({ error: "Erro ao registrar usuário" });
    }
});

app.post("/api/login", async (req, res) => {
    const { email, password } = req.body;
    const user = await User.findOne({ email });
    if (!user || !(await bcrypt.compare(password, user.password))) {
        return res.status(401).json({ error: "Credenciais inválidas" });
    }
    // Aqui incluímos email no token para validar admin depois
    const token = jwt.sign({ id: user._id, email: user.email }, "secret", { expiresIn: "1d" });
    res.json({ token });
});

app.get("/api/me", verifyToken, async (req, res) => {
    const user = await User.findById(req.user.id).select("-password").lean();
    if (!user) return res.status(404).json({ message: "Usuário não encontrado" });
    const isAdmin = user.email === ADMIN_EMAIL;
    res.json({ user: { ...user, isAdmin } });
});

/////////////////////
// Produtos públicos
/////////////////////

app.get("/api/products", async (req, res) => {
    const products = await Product.find();
    res.json(products);
});

/////////////////////
// Rotas Admin protegidas
/////////////////////

const isAdmin = (req, res, next) => {
    if (req.user.email !== ADMIN_EMAIL) {
        return res.status(403).json({ message: "Acesso negado" });
    }
    next();
};

app.post("/api/admin/products", verifyToken, isAdmin, async (req, res) => {
    try {
        const { name, description, price, imageUrl, stock, category } = req.body;

        if (!name || !price || !category) {
            return res.status(400).json({ message: "Campos obrigatórios faltando" });
        }

        const newProduct = new Product({ name, description, price, imageUrl, stock, category });
        await newProduct.save();

        res.status(201).json(newProduct);
    } catch (error) {
        console.error("Erro ao criar produto:", error);
        res.status(500).json({ message: "Erro ao criar produto" });
    }
});

/////////////////////
// Servidor
/////////////////////

app.listen(4000, () => {
    console.log("Servidor rodando na porta 4000");
});
////////////////////////////////////
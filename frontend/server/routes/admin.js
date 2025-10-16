const express = require("express");
const router = express.Router();
const Product = require("../models/Product");
const { verifyToken } = require("../middleware/auth");
const { isAdmin } = require("../middleware/admin");

router.post("/products", verifyToken, isAdmin, async (req, res) => {
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

module.exports = router;

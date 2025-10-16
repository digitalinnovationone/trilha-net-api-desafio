const jwt = require("jsonwebtoken");

const verifyToken = (req, res, next) => {
    const authHeader = req.headers.authorization;
    if (!authHeader) return res.status(401).json({ message: "Token não fornecido" });

    const token = authHeader.split(" ")[1];
    if (!token) return res.status(401).json({ message: "Token inválido" });

    try {
        const decoded = jwt.verify(token, process.env.JWT_SECRET || "secret");
        req.user = decoded; // Aqui tem id e email do usuário
        next();
    } catch (err) {
        return res.status(403).json({ message: "Token inválido ou expirado" });
    }
};

module.exports = { verifyToken };

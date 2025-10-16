const express = require("express");
const axios = require("axios");
const xml2js = require("xml2js");

const router = express.Router();

router.get("/", async (req, res) => {
    const { cepDestino, peso, comprimento, altura, largura } = req.query;

    if (!cepDestino) return res.status(400).json({ error: "CEP de destino é obrigatório" });

    const url = "https://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx";
    const params = {
        nCdEmpresa: "",
        sDsSenha: "",
        nCdServico: "04510", // PAC
        sCepOrigem: "01001000", // CEP da loja
        sCepDestino: cepDestino,
        nVlPeso: peso || "1",
        nCdFormato: "1",
        nVlComprimento: comprimento || "20",
        nVlAltura: altura || "10",
        nVlLargura: largura || "15",
        nVlDiametro: "0",
        sCdMaoPropria: "N",
        nVlValorDeclarado: "0",
        sCdAvisoRecebimento: "N",
        StrRetorno: "xml",
    };

    try {
        const correiosRes = await axios.get(url, { params });
        const parsed = await xml2js.parseStringPromise(correiosRes.data);
        const servico = parsed?.Servicos?.cServico?.[0];

        if (!servico) return res.status(500).json({ error: "Erro ao processar resposta dos Correios" });

        const valor = parseFloat(servico.Valor[0].replace(",", "."));
        const prazo = parseInt(servico.PrazoEntrega[0]);

        res.json({ valor, prazo });
    } catch (error) {
        console.error("Erro ao calcular frete:", error);
        res.status(500).json({ error: "Erro ao calcular frete" });
    }
});

module.exports = router;

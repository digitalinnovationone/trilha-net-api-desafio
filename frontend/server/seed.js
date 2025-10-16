const mongoose = require("mongoose");
const Product = require("./models/Product");

mongoose.connect("mongodb://localhost:27017/pelebrasileira");

const products = [
    {
        name: "Vestido Floral",
        description: "Leve e perfeito para o verão",
        price: 129.90,
        imageUrl: "https://via.placeholder.com/200x250.png?text=Vestido+Floral",
        stock: 10,
    },
    {
        name: "Camisa Oversized",
        description: "Estilo urbano, confortável",
        price: 89.90,
        imageUrl: "https://via.placeholder.com/200x250.png?text=Camisa+Oversized",
        stock: 15,
    },
    {
        name: "Calça Cargo",
        description: "Estilosa e prática",
        price: 149.90,
        imageUrl: "https://via.placeholder.com/200x250.png?text=Calça+Cargo",
        stock: 8,
    },
];

async function seed() {
    await Product.deleteMany({});
    await Product.insertMany(products);
    console.log("Produtos inseridos");
    mongoose.disconnect();
}

seed();

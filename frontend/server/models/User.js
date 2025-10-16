// server/models/User.js
const mongoose = require("mongoose");

const UserSchema = new mongoose.Schema({
    email: {
        type: String,
        unique: true,
        required: true,
    },
    password: {
        type: String,
        required: true,
    },
});

module.exports = mongoose.model("User", UserSchema);

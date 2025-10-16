import { createContext, useState, useContext } from "react";
import { AuthContext } from "./AuthContext";

export const CartContext = createContext();

export const CartProvider = ({ children }) => {
    const [cart, setCart] = useState([]);
    const { user } = useContext(AuthContext);

    const addToCart = (product) => {
        if (!user) return alert("Você precisa estar logado para adicionar ao carrinho.");

        const existing = cart.find((item) => item._id === product._id);

        if (existing) {
            // Atualiza a quantidade se o item já estiver no carrinho
            setCart((prev) =>
                prev.map((item) =>
                    item._id === product._id ? { ...item, quantity: item.quantity + 1 } : item
                )
            );
        } else {
            // Adiciona com quantidade 1
            setCart([...cart, { ...product, quantity: 1 }]);
        }
    };

    const removeFromCart = (id) => {
        setCart((prev) => prev.filter((item) => item._id !== id));
    };

    const updateQuantity = (id, newQty) => {
        setCart((prev) =>
            prev.map((item) =>
                item._id === id ? { ...item, quantity: newQty } : item
            )
        );
    };

    return (
        <CartContext.Provider value={{ cart, addToCart, removeFromCart, updateQuantity }}>
            {children}
        </CartContext.Provider>
    );
};

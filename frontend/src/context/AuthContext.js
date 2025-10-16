import { createContext, useState, useEffect } from "react";
import axios from "axios";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);

    // Busca usuário a partir do token no localStorage (apenas no load)
    const checkAuth = async () => {
        const token = localStorage.getItem("token");
        if (token) {
            try {
                const res = await axios.get("http://localhost:4000/api/me", {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setUser(res.data.user);
                localStorage.setItem("user", JSON.stringify(res.data.user));
            } catch {
                setUser(null);
                localStorage.removeItem("token");
                localStorage.removeItem("user");
            }
        }
    };

    useEffect(() => {
        checkAuth();
    }, []);

    const logout = () => {
        setUser(null);
        localStorage.removeItem("token");
        localStorage.removeItem("user");
    };

    return (
        <AuthContext.Provider value={{ user, setUser, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

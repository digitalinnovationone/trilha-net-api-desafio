import { motion } from "framer-motion";

const BotaoAnimado = ({ href = "#contato", children = "Comece Agora", className = "" }) => {
    return (
        <motion.div
            variants={{
                hidden: { opacity: 0, y: 20 },
                visible: { opacity: 1, y: 0 },
            }}
            className="mt-10"
        >
            <motion.a
                href={href}
                whileHover={{
                    scale: 1.05,
                    boxShadow: "0px 10px 30px rgba(18, 128, 186, 0.4)",
                }}
                whileTap={{ scale: 0.95 }}
                className={`inline-block bg-white text-sky-700 hover:bg-sky-100 font-semibold py-3 px-8 rounded-full shadow-lg transition-colors duration-300 dark:bg-sky-900 dark:text-white dark:hover:bg-sky-800 ${className}`}
            >
                {children}
            </motion.a>
        </motion.div>
    );
};

export default BotaoAnimado;

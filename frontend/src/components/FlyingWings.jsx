import React from "react";
import { motion } from "framer-motion";
import asaEsquerda from "../assets/asa-esquerda.png";
import asaDireita from "../assets/asa-direita.png";

const flyingAnimation = {
    translateY: [0, -8, 0, -4, 0],
    transition: {
        duration: 5,
        repeat: Infinity,
        ease: "easeInOut",
    },
};

const wingRotation = {
    rotate: ["-20deg", "-25deg", "-20deg"],
    transition: {
        duration: 5,
        repeat: Infinity,
        ease: "easeInOut",
    },
};

export default function FlyingWings({ children }) {
    return (
        <motion.div
            className="flex justify-center items-start gap-4 mb-4"
            style={{ willChange: "transform" }}
            animate={flyingAnimation}
        >
            <motion.img
                src={asaEsquerda}
                alt="Asa esquerda"
                className="w-16 h-16 object-contain"
                animate={wingRotation}
                style={{ marginBottom: "-0.5rem" }}
            />

            <motion.h2
                className="font-heading text-4xl lg:text-5xl font-bold text-sky-800 leading-none dark:text-sky-200"
                style={{ alignSelf: "center" }}
            >
                {children}
            </motion.h2>

            <motion.img
                src={asaDireita}
                alt="Asa direita"
                className="w-16 h-16 object-contain"
                animate={{
                    rotate: ["20deg", "25deg", "20deg"],
                    transition: wingRotation.transition,
                }}
                style={{ marginBottom: "-0.5rem" }}
            />
        </motion.div>
    );
}

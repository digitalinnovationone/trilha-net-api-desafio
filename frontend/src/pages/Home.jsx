import { useState, useEffect } from "react";
import { motion } from "framer-motion";
import BotaoAnimado from "../components/BotaoAnimado";
import FlyingWings from "../components/FlyingWings";

const API_URL = "https://localhost:7295/Tarefa";

const containerVariants = {
  hidden: {},
  visible: { transition: { staggerChildren: 0.2 } },
};

const itemVariants = {
  hidden: { opacity: 0, y: 40 },
  visible: { opacity: 1, y: 0, transition: { duration: 0.4 } },
};

export default function ApiTester() {
  const [tarefas, setTarefas] = useState([]);
  const [novaTarefa, setNovaTarefa] = useState({
    titulo: "",
    descricao: "",
    data: "",
    status: "Pendente",
  });
  const [editando, setEditando] = useState(null);
  const [filtro, setFiltro] = useState({
    titulo: "",
    data: "",
    status: "",
    id: "",
  });
  const [ultimaResposta, setUltimaResposta] = useState(null); // 👈 painel debug

  // 🔹 Função genérica de requisição
  const fetchJson = async (url, options = {}) => {
    try {
      const res = await fetch(url, options);
      const text = await res.text(); // captura mesmo se não for JSON
      try {
        const json = JSON.parse(text);
        setUltimaResposta(json);
        return json;
      } catch {
        setUltimaResposta(text);
        return text;
      }
    } catch (err) {
      console.error("Erro na requisição:", err);
      setUltimaResposta({ erro: err.message });
    }
  };

  // 🔹 Carrega todas as tarefas
  const carregarTarefas = async () => {
    const data = await fetchJson(`${API_URL}/ObterTodos`);
    if (Array.isArray(data)) setTarefas(data);
  };

  // 🔹 Busca por filtros
  const buscarPorTitulo = async () => {
    if (!filtro.titulo) return;
    const data = await fetchJson(`${API_URL}/ObterPorTitulo?titulo=${filtro.titulo}`);
    if (Array.isArray(data)) setTarefas(data);
  };

  const buscarPorData = async () => {
    if (!filtro.data) return;
    const data = await fetchJson(`${API_URL}/ObterPorData?data=${filtro.data}`);
    if (Array.isArray(data)) setTarefas(data);
  };

  const buscarPorStatus = async () => {
    if (!filtro.status) return;
    const data = await fetchJson(`${API_URL}/ObterPorStatus?status=${filtro.status}`);
    if (Array.isArray(data)) setTarefas(data);
  };

  const buscarPorId = async () => {
    if (!filtro.id) return;
    const data = await fetchJson(`${API_URL}/${filtro.id}`);
    if (data && !data.erro) setTarefas([data]);
  };

  // 🔹 Cria ou atualiza tarefa
  const salvarTarefa = async (e) => {
    e.preventDefault();
    const metodo = editando ? "PUT" : "POST";
    const url = editando ? `${API_URL}/${editando.id}` : API_URL;

    await fetchJson(url, {
      method: metodo,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(novaTarefa),
    });

    setNovaTarefa({ titulo: "", descricao: "", data: "", status: "Pendente" });
    setEditando(null);
    carregarTarefas();
  };

  // 🔹 Deleta tarefa
  const excluirTarefa = async (id) => {
    if (!window.confirm("Deseja realmente excluir esta tarefa?")) return;
    await fetchJson(`${API_URL}/${id}`, { method: "DELETE" });
    carregarTarefas();
  };

  // 🔹 Preenche formulário para edição
  const editarTarefa = (tarefa) => {
    setNovaTarefa(tarefa);
    setEditando(tarefa);
  };

  useEffect(() => {
    carregarTarefas();
  }, []);

  return (
    <div className="flex flex-col min-h-screen bg-gray-50 dark:bg-sky-950 dark:text-gray-100 font-body">
      <section className="relative py-16 px-6 md:px-12 lg:px-24">
        <div className="max-w-6xl mx-auto text-center mb-10">
          <FlyingWings>
            <h1 className="text-3xl md:text-4xl font-extrabold bg-gradient-to-r from-sky-500 via-sky-300 to-sky-500 bg-clip-text text-transparent animate-pulse">
              Teste sua API CRUD .NET
            </h1>
          </FlyingWings>
          <p className="text-gray-600 mt-3 dark:text-gray-300">
            Crie, visualize, edite, exclua e filtre tarefas.
          </p>
        </div>

        {/* Formulário de Criação/Edição */}
        <motion.form
          onSubmit={salvarTarefa}
          variants={itemVariants}
          initial="hidden"
          animate="visible"
          className="bg-white/80 dark:bg-sky-900/60 backdrop-blur-md shadow-xl rounded-2xl p-8 max-w-2xl mx-auto mb-12"
        >
          <h2 className="text-xl font-semibold mb-6 text-sky-700 dark:text-sky-300">
            {editando ? "Editar Tarefa" : "Nova Tarefa"}
          </h2>

          <div className="grid md:grid-cols-2 gap-6">
            <input
              type="text"
              placeholder="Título"
              value={novaTarefa.titulo}
              onChange={(e) => setNovaTarefa({ ...novaTarefa, titulo: e.target.value })}
              required
              className="p-3 rounded-lg bg-gray-100 dark:bg-sky-950/50 border border-gray-300 dark:border-sky-800"
            />
            <input
              type="date"
              value={novaTarefa.data}
              onChange={(e) => setNovaTarefa({ ...novaTarefa, data: e.target.value })}
              required
              className="p-3 rounded-lg bg-gray-100 dark:bg-sky-950/50 border border-gray-300 dark:border-sky-800"
            />
          </div>

          <textarea
            placeholder="Descrição"
            value={novaTarefa.descricao}
            onChange={(e) => setNovaTarefa({ ...novaTarefa, descricao: e.target.value })}
            required
            className="w-full p-3 mt-4 rounded-lg bg-gray-100 dark:bg-sky-950/50 border border-gray-300 dark:border-sky-800"
          />

          <select
            value={novaTarefa.status}
            onChange={(e) => setNovaTarefa({ ...novaTarefa, status: e.target.value })}
            className="w-full p-3 mt-4 rounded-lg bg-gray-100 dark:bg-sky-950/50 border border-gray-300 dark:border-sky-800"
          >
            <option>Pendente</option>
            <option>Em Andamento</option>
            <option>Concluída</option>
          </select>

          <div className="mt-6 flex justify-center">
            <BotaoAnimado type="submit" className="px-6 py-2">
              {editando ? "Salvar Alterações" : "Adicionar Tarefa"}
            </BotaoAnimado>
          </div>
        </motion.form>

        {/* 🔍 Filtros */}
        <motion.div
          variants={itemVariants}
          initial="hidden"
          animate="visible"
          className="bg-white/70 dark:bg-sky-900/50 p-6 rounded-xl shadow-md max-w-5xl mx-auto mb-10 grid sm:grid-cols-2 lg:grid-cols-4 gap-4"
        >
          <input
            type="text"
            placeholder="Filtrar por título"
            value={filtro.titulo}
            onChange={(e) => setFiltro({ ...filtro, titulo: e.target.value })}
            className="p-3 rounded-lg bg-gray-100 dark:bg-sky-950/50"
          />
          <BotaoAnimado onClick={buscarPorTitulo}>Buscar Título</BotaoAnimado>

          <input
            type="date"
            value={filtro.data}
            onChange={(e) => setFiltro({ ...filtro, data: e.target.value })}
            className="p-3 rounded-lg bg-gray-100 dark:bg-sky-950/50"
          />
          <BotaoAnimado onClick={buscarPorData}>Buscar Data</BotaoAnimado>

          <select
            value={filtro.status}
            onChange={(e) => setFiltro({ ...filtro, status: e.target.value })}
            className="p-3 rounded-lg bg-gray-100 dark:bg-sky-950/50"
          >
            <option value="">Status</option>
            <option value="0">Pendente</option>
            <option value="1">Em Andamento</option>
            <option value="2">Concluída</option>
          </select>
          <BotaoAnimado onClick={buscarPorStatus}>Buscar Status</BotaoAnimado>

          <input
            type="number"
            placeholder="Buscar por ID"
            value={filtro.id}
            onChange={(e) => setFiltro({ ...filtro, id: e.target.value })}
            className="p-3 rounded-lg bg-gray-100 dark:bg-sky-950/50"
          />
          <BotaoAnimado onClick={buscarPorId}>Buscar ID</BotaoAnimado>
        </motion.div>

        {/* 🧾 Painel de respostas da API */}
        <div className="max-w-6xl mx-auto mb-12 bg-gray-100 dark:bg-sky-900 p-6 rounded-xl shadow-inner overflow-auto">
          <h3 className="text-lg font-semibold text-sky-700 dark:text-sky-300 mb-2">
            Última resposta da API
          </h3>
          <pre className="text-sm bg-black/80 text-green-400 p-4 rounded-lg overflow-x-auto whitespace-pre-wrap">
            {ultimaResposta
              ? JSON.stringify(ultimaResposta, null, 2)
              : "Nenhuma resposta ainda..."}
          </pre>
        </div>

        {/* Lista de Tarefas */}
        <motion.div
          variants={containerVariants}
          initial="hidden"
          animate="visible"
          className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3 max-w-6xl mx-auto"
        >
          {tarefas.map((tarefa) => (
            <motion.div
              key={tarefa.id}
              variants={itemVariants}
              className="bg-white/80 dark:bg-sky-900/60 p-6 rounded-xl shadow-lg border border-gray-200 dark:border-sky-800 flex flex-col justify-between"
            >
              <div>
                <h3 className="text-lg font-bold text-sky-700 dark:text-sky-300">
                  {tarefa.titulo}
                </h3>
                <p className="text-gray-600 mt-2 dark:text-gray-300">{tarefa.descricao}</p>
                <p className="text-sm text-gray-500 mt-2 dark:text-gray-400">
                  📅 {new Date(tarefa.data).toLocaleDateString()}
                </p>
                <span
                  className={`inline-block mt-3 px-3 py-1 text-xs rounded-full ${
                    tarefa.status === "Concluída"
                      ? "bg-green-200 text-green-700"
                      : tarefa.status === "Em Andamento"
                      ? "bg-yellow-200 text-yellow-700"
                      : "bg-gray-200 text-gray-700"
                  }`}
                >
                  {tarefa.status}
                </span>
              </div>

              <div className="flex justify-end gap-3 mt-4">
                <button
                  onClick={() => editarTarefa(tarefa)}
                  className="px-3 py-1 rounded-md text-sm bg-blue-500 hover:bg-blue-600 text-white"
                >
                  Editar
                </button>
                <button
                  onClick={() => excluirTarefa(tarefa.id)}
                  className="px-3 py-1 rounded-md text-sm bg-red-500 hover:bg-red-600 text-white"
                >
                  Excluir
                </button>
              </div>
            </motion.div>
          ))}
        </motion.div>
      </section>
    </div>
  );
}

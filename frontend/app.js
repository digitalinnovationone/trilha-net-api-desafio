const API_URL = localStorage.getItem('apiUrl') || 'http://localhost:5181';

const elements = {
  form: document.getElementById('taskForm'),
  id: document.getElementById('taskId'),
  titulo: document.getElementById('titulo'),
  descricao: document.getElementById('descricao'),
  data: document.getElementById('data'),
  status: document.getElementById('status'),
  cancelEdit: document.getElementById('cancelEdit'),
  toast: document.getElementById('toast'),
  tableBody: document.getElementById('tasksTableBody'),
  template: document.getElementById('taskRowTemplate'),
  filtroTitulo: document.getElementById('filtroTitulo'),
  filtroData: document.getElementById('filtroData'),
  filtroStatus: document.getElementById('filtroStatus'),
  buscarTitulo: document.getElementById('buscarTitulo'),
  buscarData: document.getElementById('buscarData'),
  buscarStatus: document.getElementById('buscarStatus'),
  reloadTasks: document.getElementById('reloadTasks'),
  apiUrlDisplay: document.getElementById('apiUrlDisplay')
};

document.addEventListener('DOMContentLoaded', () => {
  setupForm();
  setupFilters();
  renderApiUrl();
  loadTasks();
});

function renderApiUrl() {
  elements.apiUrlDisplay.textContent = `API: ${API_URL}`;
}

function setupForm() {
  elements.form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const payload = {
      titulo: elements.titulo.value.trim(),
      descricao: elements.descricao.value.trim(),
      data: new Date(elements.data.value).toISOString(),
      status: elements.status.value
    };

    if (!payload.titulo || !payload.descricao || !elements.data.value) {
      showToast('Preencha todos os campos obrigatórios.');
      return;
    }

    try {
      if (elements.id.value) {
        await updateTask(elements.id.value, payload);
        showToast('Tarefa atualizada com sucesso!');
      } else {
        await createTask(payload);
        showToast('Tarefa criada com sucesso!');
      }
      resetForm();
      await loadTasks();
    } catch (error) {
      handleError(error);
    }
  });

  elements.cancelEdit.addEventListener('click', resetForm);
}

function setupFilters() {
  elements.buscarTitulo.addEventListener('click', () =>
    fetchTasks(`/Tarefa/ObterPorTitulo?titulo=${encodeURIComponent(elements.filtroTitulo.value.trim())}`)
  );

  elements.buscarData.addEventListener('click', () => {
    const date = elements.filtroData.value;
    if (!date) {
      showToast('Informe uma data para buscar.');
      return;
    }
    fetchTasks(`/Tarefa/ObterPorData?data=${date}`);
  });

  elements.buscarStatus.addEventListener('click', () => {
    const status = elements.filtroStatus.value;
    if (!status) {
      loadTasks();
      return;
    }
    fetchTasks(`/Tarefa/ObterPorStatus?status=${status}`);
  });

  elements.reloadTasks.addEventListener('click', () => {
    elements.filtroTitulo.value = '';
    elements.filtroData.value = '';
    elements.filtroStatus.value = '';
    loadTasks();
  });
}

async function loadTasks() {
  return fetchTasks('/Tarefa/ObterTodos');
}

async function fetchTasks(path) {
  try {
    const response = await fetch(`${API_URL}${path}`);
    if (!response.ok) {
      throw new Error('Não foi possível obter as tarefas.');
    }

    const data = await response.json();
    renderTasks(Array.isArray(data) ? data : [data]);
  } catch (error) {
    handleError(error);
  }
}

async function createTask(payload) {
  const response = await fetch(`${API_URL}/Tarefa`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload)
  });

  if (!response.ok) {
    throw new Error('Erro ao criar a tarefa.');
  }
}

async function updateTask(id, payload) {
  const response = await fetch(`${API_URL}/Tarefa/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload)
  });

  if (!response.ok) {
    throw new Error('Erro ao atualizar a tarefa.');
  }
}

async function deleteTask(id) {
  const response = await fetch(`${API_URL}/Tarefa/${id}`, {
    method: 'DELETE'
  });

  if (!response.ok) {
    throw new Error('Erro ao excluir a tarefa.');
  }
}

function renderTasks(tasks) {
  const tbody = elements.tableBody;
  tbody.innerHTML = '';

  if (!tasks || tasks.length === 0) {
    tbody.innerHTML = '<tr><td colspan="5" class="empty">Nenhuma tarefa encontrada.</td></tr>';
    return;
  }

  const fragment = document.createDocumentFragment();

  tasks.forEach((task) => {
    const clone = elements.template.content.cloneNode(true);
    const row = clone.querySelector('tr');

    row.dataset.id = task.id;
    row.querySelector('[data-field="titulo"]').textContent = task.titulo;
    row.querySelector('[data-field="descricao"]').textContent = task.descricao;
    row.querySelector('[data-field="status"]').textContent = task.status;
    row.querySelector('[data-field="data"]').textContent = formatDate(task.data);

    const editButton = row.querySelector('.edit');
    const deleteButton = row.querySelector('.delete');

    editButton.addEventListener('click', () => populateForm(task));
    deleteButton.addEventListener('click', async () => {
      if (confirm(`Deseja excluir a tarefa "${task.titulo}"?`)) {
        try {
          await deleteTask(task.id);
          showToast('Tarefa excluída.');
          await loadTasks();
        } catch (error) {
          handleError(error);
        }
      }
    });

    fragment.appendChild(clone);
  });

  tbody.appendChild(fragment);
}

function populateForm(task) {
  elements.id.value = task.id;
  elements.titulo.value = task.titulo;
  elements.descricao.value = task.descricao;
  elements.status.value = task.status;
  elements.data.value = task.data ? formatForInput(task.data) : '';
  elements.cancelEdit.classList.remove('hidden');
  elements.titulo.focus();
}

function resetForm() {
  elements.form.reset();
  elements.id.value = '';
  elements.cancelEdit.classList.add('hidden');
}

function showToast(message) {
  elements.toast.textContent = message;
  elements.toast.classList.add('show');
  setTimeout(() => elements.toast.classList.remove('show'), 2500);
}

function handleError(error) {
  console.error(error);
  showToast(error.message || 'Ocorreu um erro inesperado.');
}

function formatDate(value) {
  if (!value) return '—';
  const date = new Date(value);
  return new Intl.DateTimeFormat('pt-BR', {
    dateStyle: 'short',
    timeStyle: 'short'
  }).format(date);
}

function formatForInput(value) {
  const date = new Date(value);
  const pad = (num) => String(num).padStart(2, '0');
  return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}T${pad(date.getHours())}:${pad(date.getMinutes())}`;
}

// Permite alterar a URL da API rapidamente através de um prompt
window.addEventListener('keydown', (event) => {
  if (event.ctrlKey && event.key.toLowerCase() === 'u') {
    const newUrl = prompt('Informe a URL base da API', API_URL);
    if (newUrl) {
      localStorage.setItem('apiUrl', newUrl);
      window.location.reload();
    }
  }
});


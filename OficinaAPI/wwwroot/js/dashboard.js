// 6 Mecânicos solicitados (4 homens e 2 mulheres com nomes comuns brasileiros)
const mecanicos = ["Carlos Souza", "Ricardo Alves", "Marcos Oliveira", "André Santos", "Mariana Costa", "Juliana Lima"];

document.addEventListener('DOMContentLoaded', () => {
    if (localStorage.getItem('adminLogado') !== 'true') { window.location.href = 'index.html'; }
    carregarPainel();
});

async function carregarPainel() {
    const response = await fetch('/api/ordensservicos');
    const ordens = await response.json();
    const tbodyAtivos = document.querySelector('#tabelaAtivos tbody');
    const tbodyFinalizados = document.querySelector('#tabelaFinalizados tbody');

    tbodyAtivos.innerHTML = ''; tbodyFinalizados.innerHTML = '';

    ordens.forEach(os => {
        if (os.status === "Finalizado") {
            tbodyFinalizados.innerHTML += `
                <tr>
                    <td><strong>${os.numeroOS}</strong></td>
                    <td>${os.nomeCliente}</td>
                    <td>${os.marcaCarro} - ${os.modeloCarro}</td>
                    <td>${os.tipoServico}</td>
                    <td>👨‍🔧 ${os.mecanicoResponsavel}</td>
                    <td class="comentario-txt">"${os.comentarioFinal}"</td>
                </tr>`;
        } else {
            let acaoHtml = ''; let mecanicoHtml = '';
            if (os.status === "Pendente") {
                let options = '<option value="">Escolha...</option>';
                mecanicos.forEach(m => options += `<option value="${m}">${m}</option>`);
                mecanicoHtml = `<select id="sel-${os.id}">${options}</select>`;
                acaoHtml = `<button class="btn-iniciar" onclick="iniciarServico(${os.id})">Iniciar Trabalho</button>`;
            } else if (os.status === "Em Andamento") {
                mecanicoHtml = `👨‍🔧 <strong>${os.mecanicoResponsavel}</strong>`;
                acaoHtml = `
                    <div class="finalizar-box">
                        <input type="text" id="coment-${os.id}" placeholder="Observações mecânicas...">
                        <button class="btn-concluir" onclick="finalizarServico(${os.id})">Concluir OS</button>
                    </div>`;
            }
            tbodyAtivos.innerHTML += `
                <tr>
                    <td><strong>${os.numeroOS}</strong></td>
                    <td>${os.nomeCliente}</td>
                    <td>${os.marcaCarro} - ${os.modeloCarro}</td>
                    <td>${os.tipoServico}</td>
                    <td><span class="badge ${os.status === 'Em Andamento' ? 'em' : 'pendente'}">${os.status}</span></td>
                    <td>${mecanicoHtml}</td>
                    <td>${acaoHtml}</td>
                </tr>`;
        }
    });
}

async function iniciarServico(id) {
    const mec = document.getElementById(`sel-${id}`).value;
    if (!mec) return alert("Selecione um mecânico!");
    await fetch(`/api/ordensservicos/atribuir/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ mecanico: mec })
    });
    carregarPainel();
}

async function finalizarServico(id) {
    const coment = document.getElementById(`coment-${id}`).value;
    if (!coment) return alert("Digite um comentário de encerramento!");
    await fetch(`/api/ordensservicos/finalizar/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ comentario: coment })
    });
    carregarPainel();
}

document.getElementById('btnSair').addEventListener('click', () => {
    localStorage.removeItem('adminLogado');
    window.location.href = 'index.html';
});
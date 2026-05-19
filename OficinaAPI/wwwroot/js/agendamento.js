const selectMarca = document.getElementById('marca');
const selectModelo = document.getElementById('modelo');
const form = document.getElementById('formAgendamento');

// 1. Carrega as marcas da API FIPE assim que a página abre
async function carregarMarcas() {
    try {
        const response = await fetch('https://parallelum.com.br/fipe/api/v1/carros/marcas');
        if (!response.ok) throw new Error('Falha ao buscar marcas na FIPE');

        const marcas = await response.json();
        selectMarca.innerHTML = '<option value="">Selecione a marca</option>';

        marcas.forEach(m => {
            let opt = document.createElement('option');
            opt.value = m.codigo;
            opt.textContent = m.nome;
            selectMarca.appendChild(opt);
        });
    } catch (e) {
        console.error('Erro FIPE:', e);
        selectMarca.innerHTML = '<option value="">Erro ao carregar marcas</option>';
    }
}

// 2. Carrega os modelos quando o usuário escolhe uma marca
selectMarca.addEventListener('change', async () => {
    const codMarca = selectMarca.value;
    if (!codMarca) {
        selectModelo.innerHTML = '<option value="">Selecione a marca primeiro</option>';
        selectModelo.disabled = true;
        return;
    }

    selectModelo.disabled = true;
    selectModelo.innerHTML = '<option>Carregando modelos...</option>';

    try {
        const response = await fetch(`https://parallelum.com.br/fipe/api/v1/carros/marcas/${codMarca}/modelos`);
        if (!response.ok) throw new Error('Falha ao buscar modelos na FIPE');

        const dados = await response.json();
        selectModelo.innerHTML = '<option value="">Selecione o modelo</option>';

        dados.modelos.forEach(mod => {
            let opt = document.createElement('option');
            opt.value = mod.codigo;
            opt.textContent = mod.nome;
            selectModelo.appendChild(opt);
        });
        selectModelo.disabled = false;
    } catch (e) {
        console.error('Erro FIPE Modelos:', e);
        selectModelo.innerHTML = '<option>Erro ao buscar modelos</option>';
    }
});

// 3. Captura o envio do formulário e faz o POST para a Controller C#
if (form) {
    form.addEventListener('submit', async (e) => {
        e.preventDefault(); // Impede a página de recarregar

        const msgSucesso = document.getElementById('msgSucesso');
        msgSucesso.style.color = '#34495e';
        msgSucesso.innerHTML = 'Processando seu agendamento...';

        // Monta o objeto exatamente como a classe OrdemServico no C# espera
        const dados = {
            nomeCliente: document.getElementById('nome').value,
            cpf: document.getElementById('cpf').value,
            email: document.getElementById('email').value,
            tipoServico: document.getElementById('servico').value,
            marcaCarro: selectMarca.options[selectMarca.selectedIndex].text,
            modeloCarro: selectModelo.options[selectModelo.selectedIndex].text
        };

        try {
            const response = await fetch('/api/ordensservicos', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(dados)
            });

            if (response.ok) {
                const res = await response.json();
                msgSucesso.style.color = '#27ae60';
                msgSucesso.innerHTML = `Solicitação Enviada com Sucesso!<br>Sua Ordem de Serviço é a Nº: <strong>${res.numeroOS}</strong>`;

                // Limpa os campos do formulário após o sucesso
                form.reset();
                selectModelo.disabled = true;
                selectModelo.innerHTML = '<option value="">Selecione a marca primeiro</option>';
            } else {
                const textErro = await response.text();
                msgSucesso.style.color = '#e74c3c';
                msgSucesso.innerHTML = `Erro no servidor: Não foi possível salvar o agendamento.`;
                console.error('Resposta de erro do servidor:', textErro);
            }
        } catch (err) {
            msgSucesso.style.color = '#e74c3c';
            msgSucesso.innerHTML = 'Erro de rede: Certifique-se de que o backend C# está rodando.';
            console.error('Erro na requisição FETCH:', err);
        }
    });
}

// Inicializa a listagem de marcas ao carregar o arquivo
carregarMarcas();
document.getElementById('loginForm').addEventListener('submit', async (e) => {
    e.preventDefault(); // Impede a página de recarregar

    const email = document.getElementById('email').value;
    const senha = document.getElementById('senha').value;
    const mensagemErro = document.getElementById('mensagemErro');

    try {
        // Envia os dados para a nossa API do C#
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, senha })
        });

        const dados = await response.json();

        if (response.ok) {
            // Se o login der certo, salva um sinalizador e vai para a tela de cadastros
            localStorage.setItem('adminLogado', 'true');
            window.location.href = 'dashboard.html';
        } else {
            // Mostra o erro retornado pela API
            mensagemErro.textContent = dados.message || 'Erro ao realizar login.';
        }
    } catch (error) {
        mensagemErro.textContent = 'Não foi possível conectar ao servidor backend.';
    }
});
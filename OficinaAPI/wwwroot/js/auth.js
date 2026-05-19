document.getElementById('loginForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const senha = document.getElementById('senha').value;
    const mensagemErro = document.getElementById('mensagemErro');

    const response = await fetch('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, senha })
    });
    const dados = await response.json();

    if (response.ok) {
        localStorage.setItem('adminLogado', 'true');
        window.location.href = 'dashboard.html';
    } else {
        mensagemErro.textContent = dados.message;
    }
});
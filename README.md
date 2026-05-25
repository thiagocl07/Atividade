<<<<<<< HEAD
AtividadeThiago

Instruções para versionar este projeto e enviá-lo ao GitHub.

Pré-requisitos
- Git (Windows): https://git-scm.com/download/win
- (Opcional) GitHub CLI `gh`: https://github.com/cli/cli#installation

Passos rápidos
1. Instale o Git (e feche/reabra o terminal se necessário).
2. Opcional: crie um repositório no GitHub ou instale `gh` para criar automaticamente.
3. No terminal do projeto, rode (PowerShell):

```
pwsh -ExecutionPolicy Bypass -File .\init_repo.ps1 -RepoUrl "https://github.com/SEU_USUARIO/SEU_REPO.git"
```

Se você não fornecer `-RepoUrl`, o script apenas inicializa o repositório local e cria o commit inicial.

Se preferir comandos manuais, execute:

```
git init
git add .
git commit -m "Initial commit"
# criar o repositório no GitHub (manual) e então:
git remote add origin https://github.com/SEU_USUARIO/SEU_REPO.git
git branch -M main
git push -u origin main
```

Se quiser que eu crie o repositório no GitHub para você, instale o Git e o GitHub CLI (`gh`) e me avise — eu posso rodar os comandos aqui.

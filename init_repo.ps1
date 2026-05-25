param(
    [string]$RepoUrl = "",
    [string]$CommitMessage = "Initial commit",
    [string]$Branch = "main"
)

function Check-Command {
    param([string]$Name)
    return (Get-Command $Name -ErrorAction SilentlyContinue) -ne $null
}

if (-not (Check-Command -Name git)) {
    Write-Error "Git não está disponível na PATH. Instale Git antes de executar este script: https://git-scm.com/download/win"
    exit 1
}

if (-not (Test-Path .git)) {
    git init
} else {
    Write-Host "Repositório Git já inicializado."
}

git add .
git commit -m $CommitMessage --allow-empty

if ($RepoUrl -ne "") {
    if ((git remote) -contains 'origin') {
        Write-Host "Remote 'origin' já existe; atualizando URL..."
        git remote set-url origin $RepoUrl
    } else {
        git remote add origin $RepoUrl
    }

    git branch -M $Branch
    git push -u origin $Branch
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Falha ao empurrar para o remoto. Verifique se o URL e as credenciais estão corretos."
        exit $LASTEXITCODE
    }
    Write-Host "Push realizado com sucesso para $RepoUrl"
} else {
    Write-Host "Repositório inicializado e commit criado localmente. Forneça -RepoUrl para adicionar um remoto e empurrar."
}

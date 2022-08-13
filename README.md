# Tutorial

1. Aceder à caixa e ir a "Contas à ordem"/"Saldos e movimentos"
2. Filtrar período desejado
3. Executar script "JS/extractMovements.js" na consola de developer
4. Copiar conteudo para um ficheiro. Ex: caixa.csv
5. Correr o projeto

# Exemplo de Comando

1. dotnet run -- /Users/user/Downloads/caixa.csv LIDL false

# Argumentos

0 [Obrigatório] - Path para o ficheiro
1 [Opcional] - Filtrar movimentos por descrição. Separar keywords com "|": EX: lidl|inter
2 [Opcional] - Agrupar por descrição/tipo (credito ou debito). Default: true

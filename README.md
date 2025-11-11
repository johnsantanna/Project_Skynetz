# Project Skynetz - Sistema FaleMais

> Sistema de cálculo de tarifas telefônicas com planos FaleMais  
> Desenvolvido em **ASP.NET Core MVC (.NET 8)**  
> ℹ️ Este README foi formatado com a ajuda do github copilot

---

## Stack Tecnológica

- **Framework:** ASP.NET Core MVC 8.0
- **Linguagem:** C# 12.0
- **Banco de Dados:** SQL Server (LocalDB)
- **ORM:** Entity Framework Core
- **Frontend:** Bootstrap 5 + Bootstrap Icons
- **Localização:** pt-BR

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- SQL Server LocalDB (geralmente incluído no Visual Studio)

---

## Setup Rápido

### 1. Clone o repositório
```bash
git clone https://github.com/johnsantanna/Project_Skynetz.git
cd Project_Skynetz
```

### 2. Restaure as dependências
```bash
dotnet restore
```

### 3. Configure o banco de dados
```bash
dotnet ef database update
```

### 4. Execute o projeto
```bash
dotnet run
```

O sistema estará disponível em `https://localhost:7XXX`

---

## Estrutura do Banco

O sistema utiliza **3 tabelas principais** com dados pré-cadastrados (seed):

**Plans** - Planos disponíveis
- FaleMais 30 (30 minutos inclusos)
- FaleMais 60 (60 minutos inclusos)
- FaleMais 120 (120 minutos inclusos)

**Rates** - Tarifas por rota

| Origem | Destino | Preço/min |
|--------|---------|-----------|
| 011    | 016     | R$ 1,90   |
| 016    | 011     | R$ 2,90   |
| 011    | 017     | R$ 1,70   |
| 011    | 018     | R$ 0,90   |
| 017    | 011     | R$ 2,70   |
| 018    | 011     | R$ 1,90   |

---

## Lógica de Negócio

### Cálculo de Tarifas

**Sem Plano:**
```
Preço = Tarifa × Tempo
```

**Com Plano FaleMais:**

- **Dentro dos minutos inclusos:** `R$ 0,00`
- **Excedente:** `(Tempo - Inclusos) × (Tarifa × 1,10)`

### Exemplo Prático

> **Ligação:** 80 minutos de 011 para 016 com FaleMais 30

```
Sem Plano: 80 × 1,90 = R$ 152,00
Com Plano: (80 - 30) × (1,90 × 1,10) = R$ 104,50
Economia: R$ 47,50 (31,3%)
```

---

## Funcionalidades

### Calculadora de Tarifas
- Seleção de origem/destino por DDD
- Entrada de tempo (1-999 minutos)
- Comparação automática com/sem plano
- Exibição visual de economia

### Gestão de Tarifas (CRUD)
- Listar tarifas cadastradas
- Adicionar novas rotas
- Editar tarifas existentes
- Remover tarifas

### Interface
- Tema espacial moderno
- 100% em português brasileiro
- Formato de números: `1.234,56`
- Responsivo (mobile-first)

---

## Casos de Teste

### Teste 1: Com Economia
```diff
Origem: 011 → Destino: 016
Tempo: 80 min | Plano: FaleMais 30
+ Com Plano: R$ 104,50 
- Sem Plano: R$ 152,00  
! Economia: R$ 47,50 (31,3%)
```

### Teste 2: Economia Máxima
```diff
Origem: 011 → Destino: 016
Tempo: 20 min | Plano: FaleMais 30
+ Com Plano: R$ 0,00  
- Sem Plano: R$ 38,00  
! Economia: R$ 38,00 (100%)
```

### Teste 3: Tempo Exato
```diff
Origem: 011 → Destino: 016
Tempo: 30 min | Plano: FaleMais 30
+ Com Plano: R$ 0,00  
- Sem Plano: R$ 57,00  
! Economia: 100%
```

---

## Arquitetura

```
Project_Skynetz/
├── Controllers/          # Lógica de controle (MVC)
├── Models/              # Entidades e ViewModels
├── Data/                # Contexto EF Core + Seed
├── ModelBinders/        # Conversão vírgula/ponto
├── Views/               # Razor Pages
├── Migrations/          # Schema do banco
└── wwwroot/            # Assets estáticos
```

---

## Troubleshooting

### Erro de conexão com banco
```bash
sqllocaldb info
sqllocaldb start mssqllocaldb
```

### Migrations pendentes
```bash
dotnet ef database update
```

### Porta em uso
Edite `launchSettings.json` e altere:
```json
"applicationUrl": "https://localhost:7001;http://localhost:5001"
```

---

## Comandos Úteis

```bash
dotnet build              # Compilar
dotnet clean              # Limpar build
dotnet ef database drop   # Recriar banco
```

---

## Checklist de Validação

<details>
<summary>Clique para expandir</summary>

- [ ] Projeto compila sem erros
- [ ] Banco de dados criado automaticamente
- [ ] Seed de dados carregado (3 planos + 6 tarifas)
- [ ] Calculadora funcional
- [ ] CRUD de tarifas completo
- [ ] Validações em português
- [ ] Números formatados (vírgula decimal)
- [ ] Interface responsiva
- [ ] Navegação funcionando

</details>

---

## Contato

**Desenvolvedor:** John Santanna  
**GitHub:** [@johnsantanna](https://github.com/johnsantanna)  
**LinkedIn:** [johnsantanna](https://www.linkedin.com/in/johnsantanna)

---

<div align="center">

**Projeto desenvolvido como teste técnico**  
*ASP.NET Core MVC | .NET 8 | Entity Framework Core*

</div>

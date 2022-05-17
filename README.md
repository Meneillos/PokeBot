# PokeBot

This is a Telegram bot made just for fun. It returns data of the pokemon written in the chat.

This bot uses the great [PokéAPI](https://pokeapi.co/).

# Diagrama de flujo de la petición de variables.
---
:::mermaid
flowchart TD
    A{{Inicio Api}}
    style A stroke-dasharray:5 3
    B{{Configurar Variables}}
    style B stroke-dasharray:5 3
    C(Api Seguridad)
    D[(Base de datos)]
    subgraph API-X
        direction LR
        A.->|Método para<br>inicializar variables|B
    end    
    API-X-->|Soy X|C
    C-->|Dame las<br>variables de X|D
    D-->|Dictionary|C
    C-->|Dictionary|API-X
:::

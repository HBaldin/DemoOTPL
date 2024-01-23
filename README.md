# Como executar a demonstração

* Executar o seguinte comando no terminal

>
>docker-compose up

* Aguardar até 10 segundos para que o client de exemplo interno execute as requisições
* Acessar o caminho [http://localhost:3000](http://localhost:3000) para acessar o grafana e explorar os traces e métricas

# Limpando os dados gerados

* Executar o comando no terminal

> docker-compose down

* Apagar as pastas `obj`, `bin` e `otel-logs`

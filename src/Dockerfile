# ===============================
# Etapa 1: build
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0-bullseye AS build
WORKDIR /app

COPY . .
RUN dotnet restore API/API.csproj
RUN dotnet publish API/API.csproj -c Release -o /out

# ===============================
# Etapa 2: runtime
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bullseye
WORKDIR /app

# 🔥 Dependências nativas compatíveis com Tesseract.NET
RUN apt-get update && apt-get install -y \
    tesseract-ocr \
    libleptonica-dev \
    libtesseract-dev \
    && rm -rf /var/lib/apt/lists/*

# Copia aplicação
COPY --from=build /out .

# Copia tessdata
COPY API/tessdata ./tessdata

# Configuração do Tesseract
ENV TESSDATA_PREFIX=/app/tessdata
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "BodyMetricsAnalyzer.Api.dll"]

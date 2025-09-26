# Documentación del Flujo de Trabajo - Sistema ETL

##  Diagrama del Modelo de Datos
![Diagrama ER](Documentation/diagrama-er.png)

##  Flujo del Proceso ETL

### 1. Configuración Inicial
- Ejecutar script SQL para crear base de datos
- Verificar archivos CSV en carpeta de ejecución

### 2. Proceso de Extracción
```csharp
// Lectura de archivos CSV
var customers = extractor.ExtractCustomers("customers.csv");
var products = extractor.ExtractProducts("products.csv");
// ... más extracciones

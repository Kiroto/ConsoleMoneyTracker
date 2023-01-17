# Proyecto:
Desarrollar un Expense Tracker

## Fechas Límites:
Checkpoint: Viernes 20-Ene (hasta las 23:59:59)
Entrega: Lunes 23-Ene (AL MEDIO DÍA)

Nota: cada entrega será un link al repositorio en github
## Funcionalidad mínima requerida:
* CRUD (Create/Read/Update/Delete) de Categorías
* CRUD de Cuentas
* CRUD de Transacciones (tipo[gasto/ingreso], cuenta, categoría, monto, moneda, descripción, fecha/hora)
* Soporte de Transacciones en USD, la cual convertirían a DOP usando una tasa
    * Para obtener la tasa de USD a DOP pueden hacer un scrapping de https://www.infodolar.com.do (yo les proporcionaré un snippet de código que hace esto)
* Visualización del "balance actual" de las cuentas
* Algún tipo de reporte (no necesariamente gráfico, puede ser summaries por categorías o por cuentas, etc.)
* Pueden implementar más funcionalidades si encuentran que con estas no les permite escribir suficientes casos de pruebas relevantes

## Constraints:
* Debe estar desarrollado en C# (puede ser de consola o windows form/wpf)
* Deben usar git durante el proceso de desarrollo
* Deben implementar casos de pruebas
* Deben utilizar Asincronía como mínimo para obtener la tasa de https://www.infodolar.com.do, para lo cual deberán utilizar async/await. Pueden hacerlo para mostrar un "loading" o para permitir usar el UI si su programa es WinForms/WPF, o cualquier otra alternativa que me demuestre su manejo de este tema. OPCIONALMENTE: Si encuentran otra oportunidad de concurrencia, pueden hacerlo, aunque no es requerido (por ejemplo, si ustedes agregan la posibilidad de hacer una operación en Bulk, para todas las transacciones, pudiesen usar ThreadPool.QueueUserWorkItem o Task.Run para aprovechar el paralelismo, o si deciden leer de un archivo del disco o usar una base de datos o algún recurso externo, pueden usar también asincronía para estos; Nota: la librería SqlLite de C# no soporta async).
* No necesariamente deben conectarse a una BD (incluso, no se lo recomiendo, por el poco tiempo). Pueden manejar todo en memoria, y hacer una carga inicial de las informaciones, ya sea hardcodeandolas, o leyendolas de un archivo al iniciar la aplicación.
* Deberán asegurarse de que no haya una oportunidad de refactoring muy relevante. Mientras no haya halgo demasiado obvio no habrá problemas
* Para los casos de pruebas:
  * sólo casos de pruebas que proporcionen valor, es decir, no probar cosas triviales
  * no probar varios escenarios en el mismo caso de prueba; pueden tener varios asserts, pero es para evaluar varias partes del mismo comportamiento
  * NO usar IFs, ni try/catch en los casos de pruebas (ya que en la gran mayoría de los casos no deberían ser necesarios)
  * nombrar como "sut" (System Under Testing) el objeto que está siendo probado
  * dividir el caso usando la convención AAA (Arrange, Act and Assert)
  * Adicionalmente, se verán obligados a usar Dependency Injection y crear un "Stub" para eliminar la dependencia con el HTTP request a https://www.infodolar.com.do (les proporcionaré un ejemplo)

## Criterios de evaluación:

   * 10% - Usabilidad de la aplicación (UX/UI, relativamente hablando. es decir, no tendrá puntos extra quien use un UI vs quien haga un programa de consola)
   * 10% - Uso del Git (no se preocupen por tratar de tener un git log "impecable". Sólo me fijaré que todos los del grupo lo usen)
   * 15% - Limitar los code smells 
   * 40% - Casos de pruebas unitarias
   * 25% - Uso de concurrencia (async/await, y opcionalmente Task.Run/ThreadPool.QueueUserWorkItem) 

## Ejemplos de Expense Trackers para Android (NO se compliquen...):

   * https://play.google.com/store/apps/details?id=com.oriondev.moneywallet (free and open source)
   * https://play.google.com/store/apps/details?id=com.rammigsoftware.bluecoins
   * https://play.google.com/store/apps/details?id=com.monito
   * https://play.google.com/store/apps/details?id=org.totschnig.myexpenses
   * https://play.google.com/store/apps/details?id=org.pixelrush.moneyiq
   * https://play.google.com/store/apps/details?id=com.droid4you.application.wallet

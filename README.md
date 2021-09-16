# WebService

## В проекте подключен Swagger, который доступен по ссылке https://localhost:8050/swagger/index.html
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

### **1. Добавление новой точки измерения с указанием счетчика, трансформатора тока и трансформатора напряжения.**

> **POST https://localhost:8050/api/ElectricityMeteringPoint**

**Body:**

```json
{
    "ElectricityMeter": [{
        "No": "111",
        "Type": "A1",
        "VerificationDate": "2018-02-01"
    }],
    "VoltageTransformer": [{
        "No": "222",
        "Type": "B1",
        "VerificationDate": "2018-02-01",
        "TransformationRatio": 152.4
    }],
    "ElectricalTransformer": [{
        "No": "333",
        "Type": "C1",
        "VerificationDate": "2018-02-01",
        "TransformationRatio": 152.4
    }],
    "name": "Точка измерения 5",
    "consumptionObjectID": 1
}
```

**Ответ сервера:**

```json
{
    "consumptionObjectID": 1,
    "voltageTransformer": [
        {
            "electricityMeteringPointID": 4,
            "transformationRatio": 152.4,
            "id": 1,
            "no": "222",
            "type": "B1",
            "verificationDate": "2018-02-01T00:00:00"
        }
    ],
    "electricalTransformer": [
        {
            "electricityMeteringPointID": 4,
            "transformationRatio": 152.4,
            "id": 1,
            "no": "333",
            "type": "C1",
            "verificationDate": "2018-02-01T00:00:00"
        }
    ],
    "electricityMeter": [
        {
            "electricityMeteringPointID": 4,
            "id": 4,
            "no": "111",
            "type": "A1",
            "verificationDate": "2018-02-01T00:00:00"
        }
    ],
    "meteringDevice": [],
    "id": 4,
    "name": "Точка измерения 5"
```

По завершению запроса также, помимо *ElectricityMeteringPoint*, будут отдельно созданы и записаны три новых сущности *(voltageTransformer, electricalTransformer, electricityMeter)*

### **2. Выбрать все расчетные приборы учета в 2018 году.**

> **GET https://localhost:8050/api/MeteringDevice?year=2018**

Ответ сервера:
```json
[
    {
        "id": 1,
        "no": "123",
        "startDate": "2018-12-01T00:00:00",
        "electricityMeteringPoint": []
    },
    {
        "id": 2,
        "no": "456",
        "startDate": "2018-08-15T00:00:00",
        "electricityMeteringPoint": []
    }
]
```

### **3. По указанному объекту потребления выбрать все счетчики с закончившимся сроком поверке.**

> **GET https://localhost:8050/api/ElectricityMeter?ConsumptionObjectID=1&WithExpiredDate=true**

Ответ сервера:
```json
[
    {
        "electricityMeteringPointID": 1,
        "id": 1,
        "no": "123",
        "type": "A1",
        "verificationDate": "2020-01-13T00:00:00"
    },
    {
        "electricityMeteringPointID": 2,
        "id": 2,
        "no": "456",
        "type": "B1",
        "verificationDate": "2021-02-14T00:00:00"
    }
]
```

### **4. По указанному объекту потребления выбрать все трансформаторы напряжения с закончившимся сроком поверке.**

> **GET https://localhost:8050/api/VoltageTransformer?ConsumptionObjectID=1&WithExpiredDate=true**

Ответ сервера:
```json
[
    {
        "electricityMeteringPointID": 1,
        "transformationRatio": 1.1,
        "id": 1,
        "no": "123",
        "type": "A1",
        "verificationDate": "2020-01-13T00:00:00"
    },
    {
        "electricityMeteringPointID": 2,
        "transformationRatio": 1.2,
        "id": 2,
        "no": "456",
        "type": "B1",
        "verificationDate": "2021-02-14T00:00:00"
    }
]
```

### **5. По указанному объекту потребления выбрать все трансформаторы тока с закончившимся сроком поверке.** 

> **GET https://localhost:8050/api/ElectricalTransformer?ConsumptionObjectID=1&WithExpiredDate=true**

Ответ сервера:
```json
[
    {
        "electricityMeteringPointID": 1,
        "transformationRatio": 1.1,
        "id": 1,
        "no": "123",
        "type": "A1",
        "verificationDate": "2020-01-13T00:00:00"
    },
    {
        "electricityMeteringPointID": 2,
        "transformationRatio": 1.2,
        "id": 2,
        "no": "456",
        "type": "B1",
        "verificationDate": "2021-02-14T00:00:00"
    }
]
```
# ATS IoT Documentation
# RASPBERRY PYTHON HANDLER
In the Python file main.py, we define REST API URLs for each value we retrieve from openHAB. We then connect to AWS and write the obtained values from openHAB into a JSON-formatted message, which we send to the AWS server every 5 seconds.

# IoT CORE
Here, we created certificates that allow the Python script on the Raspberry Pi to communicate with the IoT Core service. We associated the certificate with a Thing and a Policy.

Rule: 
```
SELECT * FROM 'messageTopic'
```
Publishes all data from the messageTopic to the ATS_IoT_Handler lambda.

# LAMBDAS

<details>
<summary>ATS_IoT_Handler</summary>
<br>
This lambda creates an S3 Client. The Lambda processes the input from the event of the IoT Core service and saves the string to a file stored in an S3 bucket. Only the current values are stored there.
<br>
</details>



<details>
<summary>ATS_ZCT_GET_LAMBDA</summary>
<br>
This lambda creates an S3 Client and an S3 Resource. The Lambda processes the input from a file in the S3 bucket and returns a response to the API GET method.
<br>
</details>

# API GATEWAY

This API includes a GET method that is controlled by the ATS_ZCT_GET_LAMBDA lambda. Lambda proxy integration is enabled to make it a full-fledged API.

Response:

```
response = {
        "statusCode": 200,
        "headers": {
            "Access-Control-Allow-Origin" : "*"
        },
        "body": json_data
    };

```

# TERRAFORM

This script creates a security group with allowed inbound and outbound communication on ports 80, 443, and 22. It also creates an EC2 instance of type t2.micro with Amazon Linux AMI and the private key "test". It runs a user_data script to create an NGINX web server that communicates with the API, allowing us to display API values on the server using a button.

# Multicloud
# AZURE WEBAPP FRONTEND
## HTML kód stránka vizuálna časť stránky

```atsMaster.Master``` - Created master page that includes a link to Bootstrap for visual styling in the head section and a reserved placeholder for content.

```Default.aspx``` -  HTML code of the actual page inserted in the content placeholder.

```Default.aspx.cs``` - Data insertion into the HTML code and functions for GETting data from the database and POSTing data to the database.

## Backend

```IEnvSensor.cs``` - Interface for the EnvSensorAWS and EnvSensorSQL classes.

```EnvSensorAWS.cs``` - Class for objects created by the GetLatestDataFromAWS() function in the AWSController.cs class. It adds the creation time with the Slovak time zone to the data obtained from AWS.
 

```EnvSensorSQL.cs``` -  Class for objects created by the GetFromDB(int lastNRows) function in the SQLController.cs class. Instead of creating the time in the constructor, the time is directly obtained from the Azure database.

```AWSController.cs``` 
 * private const String AWSUrl - The page where the JSON with sensor values is located.
 * *GetLatestDataFromAWS()* - Retrieves the latest data from the AWS cloud in the form of JSON, deserializes it, and saves it into an object.


```SQLController.cs```
* private const string connectionString - The string used to connect the web app to the Azure database.
* SQLQueryBuilderINSERT() - Creates a query string for inserting an object into the DB.
* SQLQueryBuilderGET() - Creates a query string for reading from the DB.
* UpdateDB(EnvSensorAWS new_env_vals) - Function that handles communication with the DB to write the latest data. The input is the object to be written into the DB.
* GetFromDB(int lastNRows) - Function that handles communication with the DB to read the last N rows from the DB. The input is the number of rows to display on the page, and the output is a list of objects retrieved from the DB.

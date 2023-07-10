import json
import urllib.parse
import boto3
import botocore
import os


print("######### LOADING FUNCTION ###########")
s3 = boto3.client('s3')



s3_bucket_name = os.environ.get('BUCKET_NAME')
s3_key_website = os.environ.get('OBJECT_NAME')


def lambda_handler(event, context):
    listOfEntries = []
    s3 = boto3.resource('s3')
    
    try:
        s3.Object(s3_bucket_name, s3_key_website).load()
    except botocore.exceptions.ClientError as e:
        if e.response['Error']['Code'] == "404":
            print(f"File ({s3_key_website})) required by this function does not exist!")
            return f"File ({s3_key_website})) required by this function does not exist!"
        else:
            raise f"File ({s3_key_website}) required by this function is not accessible!"
            print(f"File ({s3_key_website}) required by this function is not accessible!")
            return f"File ({s3_key_website}) required by this function is not accessible!"
    
  
    obj = s3.Object(s3_bucket_name, s3_key_website)
    data = obj.get()['Body'].read().decode('utf-8')
    
    
    json_data = json.loads("" + 
        data.replace("}\n{", "},\n{") + 
    "")
    
    json_data = json.dumps(json_data)
    
    response = {
        "statusCode": 200,
        "headers": {
            "Access-Control-Allow-Origin" : "*"
        },
        "body": json_data
    };
    
    print(json_data)
    return response
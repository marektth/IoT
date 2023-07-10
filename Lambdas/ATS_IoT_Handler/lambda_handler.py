import json
import boto3
import os


def upload_file(file_name, bucket, object_name=None):
    if object_name is None:
        object_name = file_name
    
    s3_client = boto3.client('s3')
    try:
        response = s3_client.upload_file(file_name, bucket, object_name)
    except ClientError as e:
        logging.error(e)
        return False
    return True


def lambda_handler(event, context):
    print("#")
    s3_client = boto3.client('s3')
  
   
    line_to_add = json.dumps(event)
    print(line_to_add)
    
   
    object_name = os.environ['OBJECT_NAME']
    bucket_name = os.environ['BUCKET_NAME']
    filepath = '/tmp/local.txt'
    
    s3_client.download_file(bucket_name, object_name, filepath)
    
    with open(filepath, 'w+', encoding = 'utf-8') as f:
        f.seek(0)
        f.write('\n' + line_to_add)
        
        f.close()
    with open(filepath, 'a+', encoding='utf-8') as f:
        f.seek(0)
        print("Printing file after modification")
        print(f.read())
        f.seek(0)
        if upload_file(filepath,bucket_name,object_name):
            print("success")
        else:
            print("fail")
    f.close()
    
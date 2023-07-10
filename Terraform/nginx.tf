provider "aws" {
  access_key = "access_key"
  secret_key = "secret_key"
  region     = "eu-central-1"
}

resource "aws_security_group" "terraform_sg" {
  description = "Allow inbound external traffic"
  name        = "sg_web"

  ingress {
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    from_port   = 80
    to_port     = 80
  }

  ingress {
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    from_port   = 22
    to_port     = 22
  }

  ingress {
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    from_port   = 443
    to_port     = 443
  }

  egress {
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    from_port   = 80
    to_port     = 80
  }

  egress {
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    from_port   = 22
    to_port     = 22
  }

  egress {
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    from_port   = 443
    to_port     = 443
  }

  tags = {
    Name = "terraform_sg"
  }
}



resource "aws_instance" "terraform_nginx_ec2" {
  ami           = "ami-0db9040eb3ab74509"
  instance_type = "t2.micro"

  user_data                   = <<-EOF
                #! /bin/bash
                sudo yum update
                sudo yum install -y httpd
                sudo chkconfig httpd on
                sudo service httpd start
                echo '<button onclick="myFunction()">Click me</button>
	            <p id="demo"></p>
	            <script>
	            function myFunction() {
  	            fetch("API URL HERE").then(res => {
   	            return res.text()
  	            }).then(text => {
    	        alert(text)
  	            })
	            }
	            </script>' | sudo tee /var/www/html/index.html
                EOF
  vpc_security_group_ids      = ["${aws_security_group.terraform_sg.id}"]
  count                       = 1
  key_name = "test"
  associate_public_ip_address = true
  tags = {
    Name        = "EC2_Terraform_Webserver_Final"
    Environment = "Test"
    Project     = "ATS_IOT"
  }
}
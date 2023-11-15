#!/bin/bash

variable_name=$1
variable_value=$2

# Check if the variable already exists
if grep -q "$variable_name=" /etc/environment; then
    # Update the existing variable
    sudo sed -i "s/^$variable_name=.*/$variable_name=$variable_value/" /etc/environment
    echo "Updated $variable_name in /etc/environment"
else
    # Add the new variable
    echo "$variable_name=$variable_value" | sudo tee -a /etc/environment
    echo "Added $variable_name to /etc/environment"
fi

# Load the new environment variables
source /etc/environment

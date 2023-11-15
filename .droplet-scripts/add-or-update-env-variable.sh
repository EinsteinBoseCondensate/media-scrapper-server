#!/bin/bash

pid_file="media-scrapper-server-pid-file.txt"

# Check if the PID file exists and is not empty
if [ -s "$pid_file" ]; then
    # Read the PID from the file
    pid=$(cat "$pid_file")

    # Check if the process is still running
    if ps -p "$pid" > /dev/null; then
        echo "Process with PID $pid is running. Killing it."
        kill "$pid"
    else
        echo "Process with PID $pid is not running."
    fi

    # Remove the PID file
    rm "$pid_file"
else
    echo "PID file is empty or does not exist. Nothing to do."
fi

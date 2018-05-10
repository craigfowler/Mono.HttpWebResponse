#!/bin/bash

WEBSERVER_PID=".xsp4.pid"

shutdown_webserver()
{
  echo "Shutting down webserver ..."
  
  if [ \! -f "$WEBSERVER_PID" ]
  then
    echo "No file was found at $WEBSERVER_PID; it seems the webserver isn't running"
    exit 1
  fi
  
  kill $(cat "$WEBSERVER_PID")
  rm "$WEBSERVER_PID"
}

shutdown_webserver
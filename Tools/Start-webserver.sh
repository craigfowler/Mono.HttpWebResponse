#!/bin/bash

SERVER_PORT="8080"
SERVER_ADDR="127.0.0.1"
WEB_APP_HOME="Mono.HttpWebResponse.WebApp"
SERVER_WEB_APP="/:."
SERVER_PID=".xsp4.pid"
APP_HOMEPAGE="http://localhost:8080/api/MyApi?success=true"
SECONDS_BETWEEN_CONNECT_ATTEMPTS="2"
MAX_ATTEMPTS="5"

app_available=1

start_webserver()
{
  echo "Starting the miniature web app with xsp4 ..."
  original_dir="$(pwd)"
  
  cd "./${WEB_APP_HOME}/"
  
  xsp4 \
    --nonstop \
    --verbose \
    --address "$SERVER_ADDR" \
    --port "$SERVER_PORT" \
    --applications "$SERVER_WEB_APP" \
    --pidfile "../$SERVER_PID" \
    &
  
  cd "$original_dir"
}

wait_for_app_to_become_available()
{
  echo "Waiting for the web app to become available ..."
  for attempt in $(seq 1 "$MAX_ATTEMPTS")
  do
    sleep "$SECONDS_BETWEEN_CONNECT_ATTEMPTS"
    echo "Connection attempt $attempt of $MAX_ATTEMPTS ..."
    try_web_app_connection
    if [ "$app_available" -eq "0" ]
    then
      echo "Connection successful!"
      break
    fi
  done
  
  if [ "$app_available" -eq "1" ]
  then
    echo "Connection to the web app was unsuccessful!" >&2
    wget \
    -T 120 \
    --content-on-error \
    -O - \
    "$APP_HOMEPAGE" \
    >&2
  fi
}

try_web_app_connection()
{
  wget \
    -T 120 \
    -q \
    -O - \
    "$APP_HOMEPAGE" \
    >/dev/null
  if [ "$?" -eq "0" ]
  then
    app_available=0
  fi
}

start_webserver
wait_for_app_to_become_available

exit "$app_available"

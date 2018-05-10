#!/bin/bash
 
NUNIT_CONSOLE_VERSION="3.8.0"
NUNIT_PATH="./packages/NUnit.ConsoleRunner.${NUNIT_CONSOLE_VERSION}/tools/nunit3-console.exe"
TESTS_ASSEMBLY_PATH="Mono.HttpWebResponse.Tests/bin/Debug/Mono.HttpWebResponse.Tests.dll"

stop_if_failure()
{
  code="$1"
  process="$2"
  if [ "$code" -ne "0" ]
  then
    echo "The process '${process}' failed with exit code $code"
    exit "$code"
  fi
}

build_solution()
{
  echo "Building the solution ..."
  msbuild

  stop_if_failure $? "Build the solution"
}

run_unit_tests()
{
  echo "Running unit tests ..."
  mono "$NUNIT_PATH" "$TESTS_ASSEMBLY_PATH"
}

build_solution
Tools/Start-webserver.sh
run_unit_tests
Tools/Stop-webserver.sh

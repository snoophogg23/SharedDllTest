#!/bin/bash
set -x #echo on
cmake --version
set +x #echo off
cmake ../ --preset linux-release
cmake --build ../out/build/linux-release
# cp ../Native/out/build/linux-release/libHello.so .
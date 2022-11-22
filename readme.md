# Shared dll Test

## CMake

    cmake --preset x64-Release  
    cmake --build out/build/x64-Release 
    dumpbin.exe /exports .\out\build\x64-Debug\Hello.dll 

## Docker

    docker build -t shareddll .
    docker run --rm shareddll

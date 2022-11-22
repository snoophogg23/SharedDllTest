#include <stdio.h>
#include <iostream>

#ifdef _WIN32
#  define EXPORTIT extern "C" __declspec(dllexport)
#else
#  define EXPORTIT extern "C"
#endif

EXPORTIT void globalHello();

namespace Hello {
  
  EXPORTIT void namespaceHello();

  class HelloWriter {
    public:
      static void staticHello();
    public:
      void sayHello();
  };
}
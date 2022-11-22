add_library (Hello 
  HelloWriter.h
  HelloWriter.cpp)

install (TARGETS Hello DESTINATION bin)
install (FILES HelloWriter.h DESTINATION include)
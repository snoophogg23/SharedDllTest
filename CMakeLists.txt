project(Hello)

add_library(${PROJECT_NAME} SHARED Native/Hello/HelloWriter.cpp)

target_include_directories(${PROJECT_NAME} PUBLIC "${CMAKE_CURRENT_SOURCE_DIR}/Native/Hello")

# enable_testing()
# add_subdirectory(tests)
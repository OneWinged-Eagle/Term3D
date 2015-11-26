cmake_minimum_required(VERSION 3.3)

set(SRCS main.cpp)

add_executable(graphic ${SRCS})

set_property(TARGET graphic PROPERTY FOLDER "executables")

install (TARGETS graphic RUNTIME DESTINATION ${PROJECT_BINARY_DIR}/bin)

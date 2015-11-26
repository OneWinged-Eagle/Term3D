cmake_minimum_required(VERSION 3.3)

set(SRCS chat_server.cpp)

add_executable(network.chat_server ${SRCS})

set_property(TARGET network.chat_server PROPERTY FOLDER "executables")

install (TARGETS network.chat_server RUNTIME DESTINATION ${PROJECT_BINARY_DIR}/bin)

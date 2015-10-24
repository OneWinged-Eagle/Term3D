cmake_minimum_required(VERSION 3.3)

set(SRCS chat_client.cpp)

add_executable(network.chat_client ${SRCS})

set_property(TARGET network.chat_client PROPERTY FOLDER "executables")

install (TARGETS network.chat_client RUNTIME DESTINATION ${PROJECT_BINARY_DIR}/bin)

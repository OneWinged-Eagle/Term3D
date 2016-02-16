CC = g++

CPPFLAGS = -I. -std=c++11 -pedantic -W -Wall -Werror -Wextra

LIB = -lboost_filesystem -lboost_signals -lboost_system -lboost_thread -lGL -lGLU -lglut -lpthread -lSDL -lSDL_image `sdl-config --cflags --libs`

#Libraries for network (temporary)
LIB_NETWORK = -lboost_system -lpthread
#End Librairies for network

NAME = Term3D

#Names for network test executables (temporary)
NAME_CLIENT = Term3D_client
NAME_SERVER = Term3D_server
#End names for network

SRC =	./main.cpp \
<<<<<<< HEAD
			./core/CommandHandler.cpp \
			./core/Core.cpp \
			./graphic/Prompt.cpp \
			./graphic/Window.cpp
=======
	./core/CommandHandler.cpp \
	./core/Core.cpp \
	./core/signalsUtils.cpp \
	./graphic/Prompt.cpp \
	./graphic/Window.cpp
>>>>>>> be88606717a8dd3ab5af1548ba21c05c776e80f6

OBJ = $(SRC:.cpp=.o)

#Sources for network (temporary)
SRC_CLIENT =	./network/Client/client.cpp
SRC_SERVER =	./network/Server/server.cpp
#End Sources for network
#OBJS for network (temporary)
OBJ_SERVER = $(SRC_SERVER:.cpp=.o)
OBJ_CLIENT = $(SRC_CLIENT:.cpp=.o)
#End OBJS for network

all: $(NAME) $(NAME_CLIENT) $(NAME_SERVER)

$(NAME):	$(OBJ)
	$(CC) -o $(NAME) $(OBJ) $(LIB)

#Instructions for network (temporary)
$(NAME_CLIENT):		$(OBJ_CLIENT)
	$(CC) -o $(NAME_CLIENT) $(OBJ_CLIENT) $(LIB_NETWORK)

$(NAME_SERVER):		$(OBJ_SERVER)
	$(CC) -o $(NAME_SERVER) $(OBJ_SERVER) $(LIB_NETWORK)
#End Instructions for network

clean:
	rm -f $(OBJ) $(OBJ_SERVER) $(OBJ_CLIENT)

fclean:	clean
	rm -f $(NAME) $(NAME_SERVER) $(NAME_CLIENT)

re: fclean all

.PHONY: all clean fclean re

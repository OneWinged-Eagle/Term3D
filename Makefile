CC = g++

CPPFLAGS = -std=c++11 -pedantic -W -Wall -Werror -Wextra

LIB = -lboost_thread -lboost_filesystem -lboost_system -lGL -lGLU -lglut -lpthread -lSDL -lSDL_image `sdl-config --cflags --libs`

NAME = Term3D

SRC =			./graphic/main.cpp \
			./graphic/Prompt.cpp \
			./graphic/Window.cpp \
			./core/CommandHandler.cpp \
			./core/Core.cpp

OBJ = $(SRC:.cpp=.o)

all: $(NAME)

$(NAME):	$(OBJ)
					$(CC) -o $(NAME) $(OBJ) $(LIB)

clean:
				rm -f $(OBJ)

fclean:	clean
				rm -f $(NAME)

re: fclean all

.PHONY: all clean fclean re

CC = g++

CPPFLAGS = -std=c++11 -W -Wall -Werror -Wextra

LIB = -lboost_filesystem -lboost_system -lGL -lGLU -lglut -lSDL -lSDL_image `sdl-config --cflags --libs`

NAME = Term3D

SRC =	./graphic/main.cpp \
			./graphic/Window.cpp \
			./graphic/Prompt.cpp \
			./core/Core.cpp \
			./core/CommandHandler.cpp

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

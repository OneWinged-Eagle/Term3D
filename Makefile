CC = g++

CPPFLAGS = -I. -std=c++11 -pedantic -W -Wall -Werror -Wextra

LIB = -lboost_filesystem -lboost_signals -lboost_system -lboost_thread -lGL -lGLU -lglut -lpthread -lSDL -lSDL_image `sdl-config --cflags --libs`

NAME = Term3D

SRC =	./main.cpp \
			./core/CommandHandler.cpp \
			./core/Core.cpp \
			./core/signalsUtils.cpp \
			./graphic/Prompt.cpp \
			./graphic/Window.cpp

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

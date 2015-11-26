CC = g++

CPPFLAGS = -std=c++11 -W -Wall -Werror -Wextra

LIB = -lboost_filesystem -lboost_system

NAME = Term3D

SRC =	./core/main.cpp \
			./core/Core.cpp \
			./core/DOS.cpp

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

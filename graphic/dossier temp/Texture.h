#ifndef _TEXTURE
#define _TEXTURE

#include <GL\glew.h>

#include <SDL.h>
#include <SDL_image.h>
#include <string>
#include <iostream>

class Texture
{
public:
	Texture();
	Texture(Texture const &textureToCopy);
	Texture(std::string imageFile);
	~Texture();

	Texture& operator=(Texture const &textureToCopy);
	bool	load();
	SDL_Surface* pixelInvert(SDL_Surface *sourceImage) const;

	GLuint getID() const;
	void	setImageFile(const std::string &imageFile);

private:
	GLuint m_id;
	std::string m_imageFile;
};

#endif // !_TEXTURE

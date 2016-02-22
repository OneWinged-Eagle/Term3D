#include "Texture.h"

Texture::Texture()
	:m_id(0), m_imageFile("")
{
}

Texture::Texture(Texture const &textureToCopy)
{
	m_imageFile = textureToCopy.m_imageFile;
	load();
}

Texture::Texture(std::string imageFile)
	: m_id(0), m_imageFile(imageFile)
{

}
Texture::~Texture()
{

	glDeleteTextures(1, &m_id);
}


Texture& Texture::operator=(Texture const &textureToCopy)
{
	m_imageFile = textureToCopy.m_imageFile;
	load();

	return *this;
}

bool	Texture::load()
{
	//load de l'image dans surface sdl
	SDL_Surface *SDLImage = IMG_Load(m_imageFile.c_str());
	if (SDLImage == 0)
	{
		std::cout << "error loading image : " << SDL_GetError() << std::endl;
		return false;
	}

	//invert de l'image
	SDL_Surface *invertedImage = pixelInvert(SDLImage);
	SDL_FreeSurface(SDLImage);

	// delete de l'anciene texture eventueuele
	if (glIsTexture(m_id) == GL_TRUE)
		glDeleteTextures(1, &m_id);

	// id genereation
	glGenTextures(1, &m_id);

	//bind
	glBindTexture(GL_TEXTURE_2D, m_id);

	//format de image

	GLenum internFormat;
	GLenum format;

	//determiner des formats pour les images a 3 compos
	if (invertedImage->format->BytesPerPixel == 3)
	{
		internFormat = GL_RGB;

		if (invertedImage->format->Rmask == 0xff)
			format = GL_RGB;
		else
			format = GL_BGR;
	}

	//la meme a 4 copos
	else if (invertedImage->format->BytesPerPixel == 4)
	{
		internFormat = GL_RGBA;

		if (invertedImage->format->Rmask == 0xff)
			format = GL_RGBA;
		else
			format = GL_BGRA;
	}

	//sinon on charge pas
	else
	{
		std::cout << "error : interf format of the image unknown" << std::endl;
		SDL_FreeSurface(invertedImage);

		return false;
	}

	//pixel copy
	glTexImage2D(GL_TEXTURE_2D, 0, internFormat, invertedImage->w, invertedImage->h, 0, format, GL_UNSIGNED_BYTE, invertedImage->pixels);

	//filtres

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);


	// unbind

	glBindTexture(GL_TEXTURE_2D, 0);


	// free des bails

	SDL_FreeSurface(invertedImage);
	return true;
}

SDL_Surface* Texture::pixelInvert(SDL_Surface *sourceImage) const
{
	// Copie imae source snas pixels

	SDL_Surface *invertedImage = SDL_CreateRGBSurface(0, 
		sourceImage->w, 
		sourceImage->h, 
		sourceImage->format->BitsPerPixel, 
		sourceImage->format->Rmask,
		sourceImage->format->Gmask, 
		sourceImage->format->Bmask, 
		sourceImage->format->Amask);

	unsigned char* sourcePixels = (unsigned char*)sourceImage->pixels;
	unsigned char* invertedPixels = (unsigned char*)invertedImage->pixels;


	//  pixels inversion

	for (int i = 0; i < sourceImage->h; i++)
	{
		for (int j = 0; j < sourceImage->w * sourceImage->format->BytesPerPixel; j++)
			invertedPixels[(sourceImage->w * sourceImage->format->BytesPerPixel * (sourceImage->h - 1 - i)) + j] = sourcePixels[(sourceImage->w * sourceImage->format->BytesPerPixel * i) + j];
	}


	return invertedImage;

}


GLuint Texture::getID() const
{
	return m_id;
}

void	Texture::setImageFile(const std::string &imageFile)
{
	m_imageFile = imageFile;
}
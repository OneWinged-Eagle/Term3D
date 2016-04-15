#ifndef _SHADER
#define	_SHADER

#include <GL\glew.h>

#include <iostream>
#include <string>
#include <fstream>

class Shader
{
public:
	Shader();
	Shader(Shader const &shaderACopier);
	Shader(std::string vertexSource, std::string fragmentSource);
	~Shader();

	Shader& operator=(Shader const &shaderToCopy);

	bool load();
	bool compileShader(GLuint &shader, GLenum type, std::string const &sourceFile);
	GLuint getProgramID() const;


private:

	GLuint m_vertexID;
	GLuint m_fragmentID;
	GLuint m_programID;

	std::string m_sourceVertex;
	std::string m_sourceFragment;
};

#endif // !_SHADER

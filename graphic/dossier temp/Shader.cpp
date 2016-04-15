#include "Shader.h"

Shader::Shader() 
	: m_vertexID(0), m_fragmentID(0), m_programID(0), m_sourceVertex(), m_sourceFragment()
{

}


Shader::Shader(Shader const &shaderToCopy)
{

	m_sourceVertex = shaderToCopy.m_sourceVertex;
	m_sourceFragment = shaderToCopy.m_sourceFragment;

	load();
}


Shader::Shader(std::string sourceVertex, std::string sourceFragment)
	: m_vertexID(0), m_fragmentID(0), m_programID(0), m_sourceVertex(sourceVertex), m_sourceFragment(sourceFragment)
{
}


Shader::~Shader()
{
	glDeleteShader(m_vertexID);
	glDeleteShader(m_fragmentID);
	glDeleteProgram(m_programID);
}



Shader& Shader::operator=(Shader const &shaderACopier)
{

	m_sourceVertex = shaderACopier.m_sourceVertex;
	m_sourceFragment = shaderACopier.m_sourceFragment;

	load();

	return *this;
}


bool	Shader::load()
{
	// on netoie au cas ou
	if (glIsShader(m_vertexID) == GL_TRUE)
		glDeleteShader(m_vertexID);

	if (glIsShader(m_fragmentID) == GL_TRUE)
		glDeleteShader(m_fragmentID);

	if (glIsProgram(m_programID) == GL_TRUE)
		glDeleteProgram(m_programID);


	//compile des shaders

	if (!compileShader(m_vertexID, GL_VERTEX_SHADER, m_sourceVertex))
		return false;

	if (!compileShader(m_fragmentID, GL_FRAGMENT_SHADER, m_sourceFragment))
		return false;


	// Cr�ation du programme

	m_programID = glCreateProgram();


	// Association des shaders

	glAttachShader(m_programID, m_vertexID);
	glAttachShader(m_programID, m_fragmentID);


	// Verrouillage des entr�es shader

	glBindAttribLocation(m_programID, 0, "in_Vertex");
	glBindAttribLocation(m_programID, 1, "in_Color");
	glBindAttribLocation(m_programID, 2, "in_TexCoord0");


	// Linkage du programme

	glLinkProgram(m_programID);


	// V�rification du linkage

	GLint erreurLink = 0;
	glGetProgramiv(m_programID, GL_LINK_STATUS, &erreurLink);


	if (erreurLink != GL_TRUE)
	{

		GLint tailleErreur = 0;
		glGetProgramiv(m_programID, GL_INFO_LOG_LENGTH, &tailleErreur);

		char *erreur = new char[tailleErreur + 1];

		glGetShaderInfoLog(m_programID, tailleErreur, &tailleErreur, erreur);
		erreur[tailleErreur] = '\0';

		std::cout << erreur << std::endl;
		delete[] erreur;
		glDeleteProgram(m_programID);

		return false;
	}

	else
		return true;
}


bool Shader::compileShader(GLuint &shader, GLenum type, std::string const &sourceFile)
{
	// Cr�ation shader

	shader = glCreateShader(type);

	if (shader == 0)
	{
		std::cout << "Erreur, le type de shader (" << type << ") n'existe pas" << std::endl;
		return false;
	}

	std::ifstream fichier(sourceFile.c_str());

	if (!fichier)
	{
		std::cout << "Erreur le fichier " << sourceFile << " est introuvable" << std::endl;
		glDeleteShader(shader);

		return false;
	}

	std::string ligne;
	std::string codeSource;

	while (getline(fichier, ligne))
		codeSource += ligne + '\n';
	fichier.close();

	const GLchar* chaineCodeSource = codeSource.c_str();


	// Envoi du code source au shader
	glShaderSource(shader, 1, &chaineCodeSource, 0);

	// Compilation du shader
	glCompileShader(shader);

	// V�rification de la compilation
	GLint erreurCompilation(0);
	glGetShaderiv(shader, GL_COMPILE_STATUS, &erreurCompilation);

	if (erreurCompilation != GL_TRUE)
	{
		GLint tailleErreur(0);
		glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &tailleErreur);

		char *erreur = new char[tailleErreur + 1];

		glGetShaderInfoLog(shader, tailleErreur, &tailleErreur, erreur);
		erreur[tailleErreur] = '\0';

		std::cout << erreur << std::endl;

		delete[] erreur;
		glDeleteShader(shader);

		return false;
	}
	else
		return true;
}
GLuint Shader::getProgramID() const
{
	return m_programID;
}

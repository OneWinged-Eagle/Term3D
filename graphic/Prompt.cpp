#include "Prompt.h"

Prompt::Prompt()
{
  setX(500);
  setY(600);
  this->curr = "";
  this->old = "";
  this->status = false;
}

Prompt::Prompt(float sx, float sy)
{
  setX(sx);
  setY(sy);
  this->curr = "";
  this->old = "";
  this->status = false;
}

void Prompt::setX(float sx)
{
  if (sx <= 0)
    this->_x = 500;
  else
    this->_x = sx;
}

void Prompt::setY(float sy)
{
  if (sy <= 0)
    this->_y = 600;
  else
    this->_y = sy;
}

std::string Prompt::getComm()
{
  return (this->old);
}

void Prompt::setComm(std::string str)//const
{
  this->old = str;
}

bool Prompt::getStatus()
{
  return this->status;
}

void Prompt::setStatus()
{
  this->status = (this->status) ? false : true;
}

std::string Prompt::getOld()
{
  return this->old;
}

void Prompt::printText(float x, float y, std::string string)
{  
  glColor4f(1.0,1.0,1.0,1.0);
  glRasterPos2i(x,y);
  glDisable(GL_TEXTURE);
  //glDisable(GL_TEXTURE_2D);

  for (int i = 0; i < (int)string.length(); i++)
    glutBitmapCharacter(GLUT_BITMAP_9_BY_15, (int)string[i]);
  glEnable(GL_TEXTURE_2D);
  //glEnable(GL_TEXTURE);
}

void Prompt::doublePrint()
{
  if (this->old != "" || this->curr != "")
    {
      printText(this->_x, this->_y + 25, this->curr);
      printText(this->_x, this->_y, this->old);
    }
}

void Prompt::parser(std::string string)
{
  std::string tmp;
      std::cout << string << std::endl;
  if (string == "return")
    {
      tmp = this->old;
      this->old = this->curr;
      this->curr = "";
      glClear(GL_COLOR_BUFFER_BIT);
    }
  if (string == "space")
    string = " ";
  
  if (string == "backspace")
    {
      glClear(GL_COLOR_BUFFER_BIT);
      this->curr = this->curr.substr(0, this->curr.size()-1);
    }

  if (string.length() == 3 && string.at(0) == '[' && string.at(2) == ']')
    string = string.at(1);
  else if(string.length() > 1 )
    string = "";
  
  this->curr = this->curr + string;
  this->doublePrint();
}


import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import { useNavigate } from 'react-router-dom';
import Login from './Login';

jest.mock('react-router-dom', () => ({
  useNavigate: jest.fn(),
}));

describe('Login', () => {
  it('renders login form', () => {
    render(<Login />);
    
    expect(screen.getByText('Logowanie')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Adres e-mail')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Hasło')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Zaloguj' })).toBeInTheDocument();
  });

  it('logs in successfully with correct credentials', () => {
    render(<Login />);
    const navigate = jest.fn();
    useNavigate.mockReturnValue(navigate);

    fireEvent.change(screen.getByPlaceholderText('Adres e-mail'), {
      target: { value: 'admin' },
    });
    fireEvent.change(screen.getByPlaceholderText('Hasło'), {
      target: { value: 'admin' },
    });
    fireEvent.click(screen.getByRole('button', { name: 'Zaloguj' }));

    expect(navigate).toHaveBeenCalledWith('/menu');
    expect(console.log).toHaveBeenCalledWith('Zalogowano pomyślnie');
  });

  it('displays error message with incorrect credentials', () => {
    render(<Login />);
    const alertMock = jest.spyOn(window, 'alert').mockImplementation(() => {});

    fireEvent.change(screen.getByPlaceholderText('Adres e-mail'), {
      target: { value: 'wrongemail' },
    });
    fireEvent.change(screen.getByPlaceholderText('Hasło'), {
      target: { value: 'wrongpassword' },
    });
    fireEvent.click(screen.getByRole('button', { name: 'Zaloguj' }));

    expect(alertMock).toHaveBeenCalledWith('Błąd logowania');
    expect(console.log).toHaveBeenCalledWith('Błąd logowania');
  });
});

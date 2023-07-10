import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import { Link } from 'react-router-dom';
import Menu from './Menu';

jest.mock('react-router-dom', () => ({
  Link: jest.fn(),
}));

describe('Menu', () => {
  it('renders menu with links', () => {
    render(<Menu />);

    expect(screen.getByText('Menu główne')).toBeInTheDocument();
    expect(screen.getByRole('link', { name: 'Raporty' })).toBeInTheDocument();
    expect(screen.getByRole('link', { name: 'Rezerwacje' })).toBeInTheDocument();
    expect(screen.getByRole('link', { name: 'Formularz rezerwacji' })).toBeInTheDocument();
    expect(screen.getByRole('link', { name: 'Stan zasobów' })).toBeInTheDocument();
    expect(screen.getByRole('link', { name: 'Pracownicy' })).toBeInTheDocument();
    expect(screen.getByRole('link', { name: 'Dodaj specjalizację' })).toBeInTheDocument();
  });

  it('logs out when "Wyloguj się" button is clicked', () => {
    render(<Menu />);
    const logoutMock = jest.fn();
    HTMLFormElement.prototype.submit = logoutMock;

    fireEvent.click(screen.getByRole('button', { name: 'Wyloguj się' }));

    expect(logoutMock).toHaveBeenCalled();
  });
});

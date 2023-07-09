import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { useNavigate } from 'react-router-dom';
import Reports from './Reports';

jest.mock('react-router-dom', () => ({
  useNavigate: jest.fn(),
}));

describe('Reports', () => {
  it('renders reports table with headers', () => {
    render(<Reports />);
    
    expect(screen.getByText('Rodzaj raportu')).toBeInTheDocument();
    expect(screen.getByText('Data od')).toBeInTheDocument();
    expect(screen.getByText('Data do')).toBeInTheDocument();
    expect(screen.getByText('Autor')).toBeInTheDocument();
    expect(screen.getByText('Opis')).toBeInTheDocument();
    expect(screen.getByText('+ Wygeneruj nowy raport')).toBeInTheDocument();
  });

  it('fetches and displays reports', async () => {
    const mockReports = [
      {
        id: 1,
        reportType: 'Report 1',
        beginDate: '2022-01-01',
        endDate: '2022-01-31',
        author: 'John Doe',
        description: 'Lorem ipsum dolor sit amet',
      },
      // Add more mock reports as needed
    ];
    jest.spyOn(window, 'fetch').mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValueOnce(mockReports),
    });

    render(<Reports />);

    await waitFor(() => {
      expect(screen.getByText('Report 1')).toBeInTheDocument();
      expect(screen.getByText('2022-01-01')).toBeInTheDocument();
      expect(screen.getByText('2022-01-31')).toBeInTheDocument();
      expect(screen.getByText('John Doe')).toBeInTheDocument();
      expect(screen.getByText('Lorem ipsum dolor sit amet')).toBeInTheDocument();
    });
  });

  it('sorts reports by column when header is clicked', () => {
    render(<Reports />);
    const mockReports = [
      {
        id: 1,
        reportType: 'Report 1',
        beginDate: '2022-01-01',
        endDate: '2022-01-31',
        author: 'John Doe',
        description: 'Lorem ipsum dolor sit amet',
      },
      // Add more mock reports as needed
    ];
    jest.spyOn(window, 'fetch').mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValueOnce(mockReports),
    });

    fireEvent.click(screen.getByText('Rodzaj raportu'));

    expect(screen.getByText('Report 1')).toBeInTheDocument();
    expect(screen.getByText('2022-01-01')).toBeInTheDocument();
    expect(screen.getByText('2022-01-31')).toBeInTheDocument();
    expect(screen.getByText('John Doe')).toBeInTheDocument();
    expect(screen.getByText('Lorem ipsum dolor sit amet')).toBeInTheDocument();
  });

  // Add more test cases as needed
});

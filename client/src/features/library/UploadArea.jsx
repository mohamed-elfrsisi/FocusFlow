// File      : client/src/features/library/UploadArea.jsx
// Developer : Yasmeen Ayman (M3)
// Reviewer  : Saeid El-Kholy (M4)
// Task(s)   : T046  -  Library UI
// Module    : FR-05 (FSD §9)
// Purpose   : Drag-and-drop PDF upload with progress bar.
import { useState } from 'react';
import api from '../../api/http';

export default function UploadArea({ subjectId, onDone }) {
  const [progress, setProgress] = useState(0);
  const [error, setError] = useState('');

  const upload = async (file) => {
    if (!file) return;
    if (file.type !== 'application/pdf') return setError('PDF files only');
    if (file.size > 10 * 1024 * 1024) return setError('Max 10 MB');
    setError('');

    const form = new FormData();
    form.append('file', file);
    form.append('title', file.name.replace(/\.pdf$/i, ''));
    if (subjectId) form.append('subjectId', subjectId);

    try {
      await api.post('/library/files', form, {
        onUploadProgress: (e) => setProgress(Math.round((e.loaded * 100) / e.total)),
      });
      setProgress(0);
      onDone();
    } catch (e) {
      setProgress(0);
      setError(e.response?.data?.error?.code ?? 'UPLOAD_FAILED');
    }
  };

  return (
    <div
      onDragOver={(e) => e.preventDefault()}
      onDrop={(e) => {
        e.preventDefault();
        upload(e.dataTransfer.files[0]);
      }}
      className="rounded-xl border-2 border-dashed border-brand-200 p-6 text-center"
    >
      <p className="text-sm text-zinc-600">Drag a PDF here or choose a file</p>
      <input
        type="file"
        accept="application/pdf"
        onChange={(e) => upload(e.target.files[0])}
        className="mt-2"
      />
      {progress > 0 && (
        <div className="mt-3 h-2 rounded bg-brand-100">
          <div className="h-2 rounded bg-brand-700" style={{ width: `${progress}%` }} />
        </div>
      )}
      {error && <p className="mt-2 text-sm text-red-600">{error}</p>}
    </div>
  );
}
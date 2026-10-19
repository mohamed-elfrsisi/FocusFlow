// File      : client/src/features/library/LinkForm.jsx
// Developer : Yasmeen Ayman (M3)
// Reviewer  : Saeid El-Kholy (M4)
// Task(s)   : T047  -  Library UI
// Module    : FR-05 (FSD §9)
// Purpose   : Add-link form validating the URL while typing.
import { useState } from 'react';
import api from '../../api/http';

export default function LinkForm({ subjectId, onDone }) {
  const [url, setUrl] = useState('');
  const [title, setTitle] = useState('');
  const [error, setError] = useState('');
  const valid = /^https?:\/\/\S+$/i.test(url);

  const submit = async (e) => {
    e.preventDefault();
    setError('');
    try {
      await api.post('/library/links', { url, title, subjectId });
      setUrl('');
      setTitle('');
      onDone();
    } catch (err) {
      setError(err.response?.data?.error?.code ?? 'LINK_FAILED');
    }
  };

  return (
    <form onSubmit={submit} className="space-y-2">
      <div className="flex gap-2">
        <input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Title"
          required
          maxLength={160}
          className="rounded-xl border border-zinc-200 px-3 py-2"
        />
        <input
          value={url}
          onChange={(e) => setUrl(e.target.value)}
          placeholder="https://..."
          className={`flex-1 rounded-xl border px-3 py-2 ${
            url && !valid ? 'border-red-500' : 'border-zinc-200'
          }`}
        />
        <button
          disabled={!valid || !title}
          className="rounded-xl bg-brand-700 px-4 text-white disabled:opacity-50"
        >
          Add link
        </button>
      </div>
      {url && !valid && (
        <p className="text-sm text-red-600">Link must start with http:// or https://</p>
      )}
      {error && <p className="text-sm text-red-600">{error}</p>}
    </form>
  );
}
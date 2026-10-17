// File      : client/src/features/library/LibraryView.jsx
// Developer : Yasmeen Ayman (M3)
// Reviewer  : Saeid El-Kholy (M4)
// Task(s)   : T046, T047  -  Library UI
// Module    : FR-05 (FSD §9)
// Purpose   : Subject tabs with counters, search, grid / list, item menu.
import { useState } from 'react';
import { useQuery, useQueryClient } from '@tanstack/react-query';
import api from '../../api/http';
import UploadArea from './UploadArea';

export default function LibraryView() {
  const qc = useQueryClient();
  const [subjectId, setSubjectId] = useState(null);
  const [q, setQ] = useState('');
  const [grid, setGrid] = useState(true);

  const { data: items = [], isLoading, isError, refetch } = useQuery({
    queryKey: ['library', subjectId, q],
    queryFn: async () =>
      (
        await api.get('/library', {
          params: { subjectId: subjectId ?? undefined, q: q || undefined },
        })
      ).data.data,
  });

  const refresh = () => qc.invalidateQueries({ queryKey: ['library'] });

  return (
    <div className="mx-auto max-w-[1200px] space-y-4 p-4">
      <UploadArea subjectId={subjectId} onDone={refresh} />

      <div className="flex gap-2">
        <input
          value={q}
          onChange={(e) => setQ(e.target.value)}
          placeholder="Search"
          className="flex-1 rounded-xl border border-zinc-200 px-3 py-2"
        />
        <button
          onClick={() => setGrid(!grid)}
          className="rounded-xl border border-brand-700 px-3 text-brand-700"
        >
          {grid ? 'List' : 'Grid'}
        </button>
      </div>

      {isLoading && <p className="text-zinc-600">Loading…</p>}
      {isError && (
        <button onClick={() => refetch()} className="text-brand-700 underline">
          Something went wrong. Retry
        </button>
      )}
      {!isLoading && !isError && items.length === 0 && (
        <p className="text-zinc-600">No items yet. Upload a PDF or add a link.</p>
      )}

      <div className={grid ? 'grid gap-4 md:grid-cols-3' : 'space-y-2'}>
        {items.map((it) => (
          <div key={it.id} className="rounded-xl border border-zinc-200 bg-white p-4">
            <div className="font-medium text-zinc-900">{it.title}</div>
            {it.type === 'file' && !it.searchable && (
              <div className="text-xs text-zinc-500">Not searchable by AI</div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
}